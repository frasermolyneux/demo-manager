targetScope = 'subscription'

// Parameters
@description('The environment for the resources')
param environment string

@description('The location to deploy the resources')
param location string

param instance string

param storageAccountName string

param tags object

// Variables
var environmentUniqueId = uniqueString('demo-manager', environment, instance)
var deploymentPrefix = 'platform-${environmentUniqueId}' //Prevent deployment naming conflicts

var resourceGroupName = 'rg-demo-manager-${environment}-${location}-${instance}'

// Platform
resource defaultResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
  tags: tags

  properties: {}
}

module storageAccount 'artifactStorage/storageAccount.bicep' = {
  name: '${deploymentPrefix}-storageAccount'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    location: location
    storageAccountName: storageAccountName
  }
}
