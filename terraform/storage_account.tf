resource "azurerm_storage_account" "artifact_storage" {
  name                = var.storage_account_name
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  tags                = var.tags

  account_tier             = "Standard"
  account_replication_type = "RAGRS"
  account_kind             = "StorageV2"
  min_tls_version          = "TLS1_2"
  access_tier              = "Hot"

  allow_nested_items_to_be_public = true
  https_traffic_only_enabled      = true

  network_rules {
    bypass         = ["AzureServices"]
    default_action = "Allow"
  }
}

resource "azurerm_storage_container" "versions" {
  name                  = "versions"
  storage_account_id    = azurerm_storage_account.artifact_storage.id
  container_access_type = "container"
}

import {
  to = azurerm_storage_account.artifact_storage
  id = "/subscriptions/${var.subscription_id}/resourceGroups/rg-${var.workload}-${var.environment}-${var.location}-${var.instance}/providers/Microsoft.Storage/storageAccounts/${var.storage_account_name}"
}

import {
  to = azurerm_storage_container.versions
  id = "/subscriptions/${var.subscription_id}/resourceGroups/rg-${var.workload}-${var.environment}-${var.location}-${var.instance}/providers/Microsoft.Storage/storageAccounts/${var.storage_account_name}/blobServices/default/containers/versions"
}
