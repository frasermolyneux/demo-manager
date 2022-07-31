targetScope = 'subscription'

// Parameters
param parLocation string
param parEnvironment string

param parLoggingSubscriptionId string
param parLoggingResourceGroupName string
param parLoggingWorkspaceName string

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

module keyVault 'br:acrmxplatformprduksouth.azurecr.io/bicep/modules/keyvault:V2022.07.31.6322' = {
  name: '${varDeploymentPrefix}-keyVault'
  scope: resourceGroup(defaultResourceGroup.name)

  params: {
    parKeyVaultName: varKeyVaultName
    parLocation: parLocation
    parTags: parTags
  }
}

module appInsights 'br:acrmxplatformprduksouth.azurecr.io/bicep/modules/appinsights:V2022.07.31.6322' = {
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
