targetScope = 'subscription'

// Parameters
param parEnvironment string
param parLocation string
param parInstance string

param parStorageAccountName string

param parTags object

// Variables
var varEnvironmentUniqueId = uniqueString('demo-manager', parEnvironment, parInstance)
var varDeploymentPrefix = 'platform-${varEnvironmentUniqueId}' //Prevent deployment naming conflicts

var varResourceGroupName = 'rg-demo-manager-${parEnvironment}-${parLocation}-${parInstance}'

// Platform
resource defaultResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: varResourceGroupName
  location: parLocation
  tags: parTags

  properties: {}
}

module storageAccount 'artifactStorage/storageAccount.bicep' = {
  name: '${varDeploymentPrefix}-storageAccount'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    parLocation: parLocation
    parStorageAccountName: parStorageAccountName
  }
}
