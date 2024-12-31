import pandas as pd
from sklearn.preprocessing import MinMaxScaler
from sklearn.decomposition import PCA
from sklearn.metrics import silhouette_score
from fancyimpute import KNN
from sklearn.cluster import AgglomerativeClustering
from scipy.cluster.hierarchy import dendrogram
from scipy.stats import mannwhitneyu
from scipy.stats import shapiro, kruskal, f_oneway
from scipy.stats import f_oneway
import matplotlib
matplotlib.use('Qt5Agg') # Install PyQt5
import matplotlib.pyplot as plt
import seaborn as sns
import numpy as np
from scipy.stats import kruskal, shapiro
from openpyxl import Workbook

# LOAD DATA 
df = pd.read_csv("C:/Users/yahya/Desktop/Group05_Project/Group05_Project/Data_reduction.csv")
print(df)
df = df.drop('gender', axis=1)
df = df.drop('marital', axis=1)

# MISSING VALUES
total_missing_count = df.isnull().sum().sum()
print("# missing values:", total_missing_count)
print(df.describe())

# HISTOGRAMS OF NUMERICAL VARIABLES
df.hist(figsize=(16, 10))
plt.suptitle("Histograms of numerical variables")
plt.show()

# CORRELATION MATRIX BETWEEN THE VARIABLES
correlation_matrix = df.corr()
plt.figure(figsize=(12, 8))
sns.heatmap(correlation_matrix, annot=True, cmap='coolwarm')
plt.title('Correlation matrix between the variables')
plt.show()

# KNN=3 TO INTERPOLATE MISSING DATA
imputer = KNN(k=3)
imputed_df = imputer.fit_transform(df)

# CONVETRS THE RESULTING ATTAY TO A DATAFRAME
imputed_df = pd.DataFrame(imputed_df, columns=df.columns)
print(imputed_df)
total_missing_count = imputed_df.isnull().sum().sum()
print("# missing values:", total_missing_count)

# MINMAXSCALER
scaler = MinMaxScaler()

# STANDARDIZE THE COLUMNS OF THE DATAFRAME
standardized_data = pd.DataFrame(scaler.fit_transform(imputed_df), columns=df.columns)
print(standardized_data)
print(standardized_data.describe())

# PCA
pca = PCA(svd_solver='full', whiten=False)
pca.fit(standardized_data) # Fit the model to the data
explained_variance_ratio = pca.explained_variance_ratio_ # Percentage of variance explained for each principal component
cumulative_variance = np.cumsum(explained_variance_ratio) # Calculate the cumulative variance
threshold = 0.85 # Threshold
num_components = np.argmax(cumulative_variance >= threshold) + 1 # Find the number of principal components to exceed or reach the threshold
print(f"Number of principal components to reach the threshold of {threshold * 100}% of variance explained: {num_components}")
pca = PCA(n_components = num_components, svd_solver='full', whiten=False)
principal_components = pca.fit_transform(standardized_data)

# PLOT OF PERCENTAGE OF VARIANCE EXPLAINED
plt.figure(figsize=(6, 4))
plt.bar(range(1, len(explained_variance_ratio) + 1), explained_variance_ratio)
plt.xlabel('Principal component')
plt.ylabel('Percentuale di Varianza Spiegata')
plt.title('Percentuale di Varianza Spiegata per Componenti Principali')
plt.show()

#PCA WEIGHT PLOT
feature_names = standardized_data.columns.tolist()
pca_components = pca.components_
pca_weights_df = pd.DataFrame(pca_components, columns=feature_names, index=[f'PC {i+1}' for i in range(num_components)]) # Creating a DataFrame for better visualization
print(pca_weights_df) # Display the weights of the principal components
n_components, n_features = pca_components.shape

# CREATE A HISTOGRAM FOR EACH PRINCIPAL COMPONENT
for i in range(n_components):
   plt.figure(figsize=(8, 6))
   plt.bar(range(n_features), pca_components[i, :], tick_label=feature_names)
   plt.xlabel('Feature')
   plt.ylabel(f'Weights PC {i + 1}')
   plt.title(f'Weights of PC {i + 1} per ciascuna feature')
   plt.xticks(range(len(feature_names)), feature_names, rotation=90)
   plt.tight_layout()
   plt.show()

# DENDROGRAM PLOT
from scipy.cluster.hierarchy import linkage
linkage_matrix = linkage(principal_components, method='ward')
dendrogram(linkage_matrix)
plt.title('Dendrogramma del Clustering Gerarchico')
plt.show()

# HIERARCHICAL CLUSTERING
n_clusters = 3
agg_clustering = AgglomerativeClustering(n_clusters, linkage='ward')
cluster_labels = agg_clustering.fit_predict(principal_components)

# Create a DataFrame with only the first 3 principal components and the Cluster column
# principal_df_3d = pd.DataFrame(data=principal_components[:, :3], columns=['PC1', 'PC2', 'PC3'])
# principal_df_3d['Cluster'] = cluster_labels
# print(principal_df_3d.head())

# Create a 3D figure
# colori = ['red', 'blue', 'green', 'orange', 'purple', 'cyan']
# fig = plt.figure()
# ax = fig.add_subplot(111, projection='3d')
# for index, row in principal_df_3d.iterrows():
#     x = row['PC1']
#     y = row['PC2']
#     z = row['PC3']
#     cluster = int(row['Cluster'])
#     colore = colori[cluster]
#     ax.scatter(x, y, z, c=colore, label=f'Cluster {cluster}')
# ax.set_xlabel('PC1')
# ax.set_ylabel('PC2')
# ax.set_zlabel('PC3')
# plt.show()

p_values = {}
for column in imputed_df.columns:
    groups = [imputed_df[column][cluster_labels == i] for i in range(n_clusters)]
    # Test for normality within each cluster using the Shapiro-Wilk test
    normality_results = [shapiro(group) for group in groups]
    # Prints normality information
    for i, result in enumerate(normality_results):
        cluster_label = i
        stat, p_value_shapiro = result
        print(f"Variable: {column}, Cluster {cluster_label}: Shapiro-Wilk Test Statistic={stat}, p-value={p_value_shapiro}")
        # Perform the Kruskal-Wallis test if the data is not approximately normal
    if any(p_value_shapiro <= 0.05 for stat, p_value_shapiro in normality_results):
        h_statistic, p_value_kruskal = kruskal(*groups)
        p_values[column] = p_value_kruskal
    else:
        f_statistic, p_value_anova = f_oneway(*groups)
        p_values[column] = p_value_anova
# Print p-values
for variable, p_value in p_values.items():
    print(f'P-value for {variable}: {p_value}')

# Determina il numero di cluster
num_clusters = len(np.unique(cluster_labels))

# Creating an empty DataFrame for the p-value array
columns = imputed_df.columns.tolist()
p_value_matrix = pd.DataFrame(index=columns, columns=['Cluster_0 vs Cluster_1', 'Cluster_0 vs Cluster_2', 'Cluster_1 vs Cluster_2'])

# Perform cluster comparison for each variable
for column in imputed_df.columns:
    for i in range(num_clusters):
        for j in range(i + 1, num_clusters):
            # Extract data for the two clusters
            group1 = imputed_df[column][cluster_labels == i]
            group2 = imputed_df[column][cluster_labels == j]

            # Perform the Mann-Whitney-U test
            stat, p_value = mannwhitneyu(group1, group2, alternative='two-sided')
            #print(f"P-value for {column} between Cluster {i} and Cluster {j}: {p_value}")

            # Assignment of the p-value to the matrix
            p_value_matrix.loc[column, f'Cluster_{i} vs Cluster_{j}'] = p_value
print(p_value_matrix)

# Create column in dataframe with clusters
df_labeled = imputed_df
df_labeled['Cluster'] = cluster_labels
print(df_labeled.head(5))

# Separate dataframes with different clusters
for cluster_id in range(n_clusters):
    df_cluster = df_labeled[df_labeled['Cluster'] == cluster_id]
    df_cluster = df_cluster.drop('Cluster', axis=1)
    globals()[f'df_cluster{cluster_id}'] = df_cluster

# CREATION A TABLE
summary_values = pd.DataFrame(index=df.columns, columns=range(n_clusters))
cluster_sizes = []
for cluster_id in range(n_clusters):
    # Select the rows of the current cluster
    cluster_rows = df_labeled[df_labeled['Cluster'] == cluster_id].drop('Cluster', axis=1)
    # Calculate the number of elements in the current cluster
    cluster_size = len(cluster_rows)
    cluster_sizes.append(cluster_size)
    for column in ['age', 'education', 'income']:
        mean_value = int(cluster_rows[column].mean())
        std_value = int(cluster_rows[column].std())
        summary_values.loc[column, cluster_id] = f"{mean_value} ({mean_value - std_value}, {mean_value + std_value})"
        # Calculate the most frequent response and percentage of responses for the other variables
        for column in df.columns.difference(['age', 'education', 'income']):
            most_frequent_response = cluster_rows[column].mode().iloc[0]
            percentage_responses = (cluster_rows[column].value_counts().iloc[0] / cluster_size) * 100
            summary_values.loc[column, cluster_id] = f"{most_frequent_response} ({percentage_responses:.2f}%)"

# Rename table columns with cluster names and number of elements
summary_values.columns = [f'Cluster {i} ({cluster_sizes[i]})' for i in range(n_clusters)]

# Add a column to the summary_values table with the p-values
summary_values['P-Values'] = p_values

print("# if cluster 0 is statistically different from cluster 1")
print("* if cluster 0 is statistically different from cluster 2")
print("° if cluster 1 is statistically different from cluster 2")
for riga in range(1, len(p_value_matrix)):
    if (p_value_matrix.iloc[riga, 0] < 0.05):
        summary_values.iloc[riga, 0] = str(summary_values.iloc[riga, 0]) + '#'
for riga in range(1, len(p_value_matrix)):
    if (p_value_matrix.iloc[riga, 1] < 0.05):
        summary_values.iloc[riga, 0] = str(summary_values.iloc[riga, 0]) + '*'
for riga in range(1, len(p_value_matrix)):
    if (p_value_matrix.iloc[riga, 2] < 0.05):
        summary_values.iloc[riga, 1] = str(summary_values.iloc[riga, 1]) + '°'

# Plot the table
print("Table of Values for Variable and Cluster:")
pd.set_option('display.max_columns', None)
pd.set_option('display.max_rows', None)
pd.set_option('display.width', None)
print(summary_values)

# Remove variables with p-value > 0.05
df_labeled=df_labeled.drop('ccs_4', axis = 1)
df_labeled=df_labeled.drop('ccs_8', axis = 1)
df_labeled=df_labeled.drop('ccs_10', axis = 1)

# Histograms of variables by cluster
for cluster_id in range(n_clusters):
    cluster_name = f"cluster_{cluster_id}"
    df_cluster = df_labeled[df_labeled['Cluster'] == cluster_id]
    df_cluster = df_cluster.drop('Cluster', axis=1)
    df_cluster.hist()
    plt.title(f"Histograms {cluster_name}")
    plt.show()

# Compute indexes
index_anx_cluster_0 = (((3)/13)*100)/3
index_skep_cluster_0 = (((0)/12)*100)/6
index_anx_cluster_1 = (((0)/13)*100)/3
index_skep_cluster_1 = (((6 + 6 + 5 + 6 + 6 + 6 + 6 + 6 + 6)/12)*100)/6
index_anx_cluster_2 = (((3 * 13)/13)*100)/3
index_skep_cluster_2 = (((0)/12)*100)/6
print('Percentuale di Eco-ansia del Cluster 0: ', index_anx_cluster_0, '%')
print('Percentuale di scetticismo climatico del Cluster 0: ', index_skep_cluster_0, '%')
print('Percentuale di Eco-ansia del Cluster 1: ', index_anx_cluster_1, '%')
print('Percentuale di scetticismo climatico del Cluster 1: ', index_skep_cluster_1, '%')
print('Percentuale di Eco-ansia del Cluster 2: ', index_anx_cluster_2, '%')
print('Percentuale di scetticismo climatico del Cluster 2: ', index_skep_cluster_2, '%')

# Create Excel file
#nome_file_excel = 'Personas_Table.xlsx'
#summary_values.to_excel(nome_file_excel, index=True)