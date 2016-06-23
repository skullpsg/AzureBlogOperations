# AzureBlogOperations
In this project,I have  created Azure blog programatically in c#.And then read the local files and uploaded them into that blob and vice versa(reading a file back from azure blob and created file locally).

# Environments
1.vs 2015

# How to work with

1.download the Project.

2.change the connection string of "StorageConnectionString" in Web.config.

  connection string expects Azure storageaccount name and its key.If you have no account then to get that you have to create storage account in azure.

  Go here : https://portal.azure.com/

  Then In filters type storage account --> Create an account --> After creating account click that storage account --> settings --> Access Key. you can be able to see the storage account name and key.
  
  3.configure that and Run the project.
  
  # Packages need to be Installed
  
  There are two packages that you'll need to install to your project:

  Microsoft Azure Storage Client Library for .NET: This package provides programmatic access to data resources in your storage account.
  Click here : https://www.nuget.org/packages/WindowsAzure.Storage/
  
  Microsoft Azure Configuration Manager library for .NET: This package provides a class for parsing a connection string from a configuration file, regardless of where your application is running.
  
  Click here : https://www.nuget.org/packages/Microsoft.WindowsAzure.ConfigurationManager/
   
