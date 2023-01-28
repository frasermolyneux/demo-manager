targetScope = 'subscription'

// Parameters
param parLocation string
param parEnvironment string

param parLoggingSubscriptionId string
param parLoggingResourceGroupName string
param parLoggingWorkspaceName string

param parKeyVaultCreateMode string = 'recover'

param parTags object

// Variables
var varDeploymentPrefix = 'platformDemoManager' //Prevent deployment naming conflicts
var varResourceGroupName = 'rg-demomanager-${parEnvironment}-${parLocation}'
var varKeyVaultName = 'kv-demomgr-${parEnvironment}-${parLocation}'
var varAppInsightsName = 'ai-demomanager-${parEnvironment}-${parLocation}'

// Platform
resource defaultResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: varResourceGroupName
  location: parLocation
  tags: parTags

  properties: {}
}

module keyVault 'br:acrty7og2i6qpv3s.azurecr.io/bicep/modules/keyvault:latest' = {
  name: '${varDeploymentPrefix}-keyVault'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    parKeyVaultName: varKeyVaultName
    parLocation: parLocation
    parKeyVaultCreateMode: parKeyVaultCreateMode
    parTags: parTags
  }
}

module appInsights 'br:acrty7og2i6qpv3s.azurecr.io/bicep/modules/appinsights:latest' = {
  name: '${varDeploymentPrefix}-appInsights'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    parAppInsightsName: varAppInsightsName
    parKeyVaultName: keyVault.outputs.outKeyVaultName
    parLocation: parLocation
    parLoggingSubscriptionId: parLoggingSubscriptionId
    parLoggingResourceGroupName: parLoggingResourceGroupName
    parLoggingWorkspaceName: parLoggingWorkspaceName
    parTags: parTags
  }
}

module storageAccount 'artifactStorage/storageAccount.bicep' = {
  name: '${varDeploymentPrefix}-storageAccount'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    parLocation: parLocation
  }
}
