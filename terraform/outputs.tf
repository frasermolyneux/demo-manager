output "resource_group_name" {
  value = data.azurerm_resource_group.rg.name
}

output "storage_account_name" {
  value = azurerm_storage_account.artifact_storage.name
}

output "app_insights_connection_string" {
  value     = azurerm_application_insights.ai.connection_string
  sensitive = true
}
