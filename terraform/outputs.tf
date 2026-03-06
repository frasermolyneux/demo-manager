output "resource_group_name" {
  value = azurerm_resource_group.default.name
}

output "storage_account_name" {
  value = azurerm_storage_account.artifact_storage.name
}
