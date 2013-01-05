﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolcore.Repository.Tests.Mocks;
using System.Diagnostics;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Rolcore.Repository.WindowsAzure.StorageClientImpl;

namespace Rolcore.Repository.Tests.WindowsAzure.StorageClient
{
    [TestClass]
    public class TableServiceContextRepositoryTest : IRepositoryTests<IRepository<MockEntity<DateTime>, DateTime>, DateTime>
    {
        const string MocksTableName = "Mocks";
        const string StorageConnectionString = "UseDevelopmentStorage=true";
        const string CsrunPath = @"C:\Program Files\Microsoft SDKs\Windows Azure\Emulator\csrun.exe"; //TODO: Do not hard-code path

        #region Additional test attributes

        [ClassInitialize()]
        public static void TableServiceContextRepositoryTestClassInitialize(TestContext testContext)
        {
            // command line reference for csrun.exe: http://msdn.microsoft.com/en-us/library/windowsazure/gg433001.aspx

            var start = new ProcessStartInfo
            {
                Arguments = "/devstore:start /devfabric:start",
                FileName = CsrunPath
            };

            var proc = new Process { StartInfo = start };
            proc.Start();
            proc.WaitForExit();
        }

        [ClassCleanup()]
        public static void TableServiceContextRepositoryTestClassCleanup()
        {
            var start = new ProcessStartInfo
            {
                Arguments = "/devstore:shutdown /devfabric:shutdown",
                FileName = CsrunPath
            };

            var proc = new Process { StartInfo = start };
            proc.Start();
            proc.WaitForExit();
        }
        #endregion

        protected override IRepository<MockEntity<DateTime>, DateTime> CreateTargetRepository()
        {
            // Retrieve storage account from connection-string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            tableClient.CreateTableIfNotExist(MocksTableName);

            // Get the data service context
            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            return new Repository<MockEntity<DateTime>, DateTime>(
                new TableServiceContextRepositoryReader<MockEntity<DateTime>>(serviceContext, MocksTableName),
                new TableServiceContextRepositoryWriter<MockEntity<DateTime>>(serviceContext, MocksTableName));
        }

        protected override void ClearTestData()
        {
            // Retrieve storage account from connection-string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            if (tableClient.DoesTableExist(MocksTableName))
                tableClient.DeleteTable(MocksTableName);
            tableClient.CreateTableIfNotExist(MocksTableName);
        }
    }
}
