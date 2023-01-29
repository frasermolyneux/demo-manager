targetScope = 'resourceGroup'

// Parameters
param parStorageAccountName string
param parLocation string

resource storageAccounts 'Microsoft.Storage/storageAccounts@2021-09-01' = {
  name: parStorageAccountName
  location: parLocation

  sku: {
    name: 'Standard_RAGRS'
  }

  kind: 'StorageV2'

  properties: {
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: true

    networkAcls: {
      bypass: 'AzureServices'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }

    supportsHttpsTrafficOnly: true

    encryption: {
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }

    accessTier: 'Hot'
  }
}

resource blobServices 'Microsoft.Storage/storageAccounts/blobServices@2021-09-01' = {
  parent: storageAccounts
  name: 'default'

  properties: {
    cors: {
      corsRules: []
    }

    deleteRetentionPolicy: {
      allowPermanentDelete: false
      enabled: false
    }
  }
}

resource containers 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-09-01' = {
  parent: blobServices
  name: 'versions'

  properties: {
    immutableStorageWithVersioning: {
      enabled: false
    }

    publicAccess: 'Container'
  }
}
