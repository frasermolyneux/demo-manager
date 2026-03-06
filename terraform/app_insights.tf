resource "azurerm_application_insights" "ai" {
  name                = "ai-${var.workload}-${var.environment}-${var.location}"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
  workspace_id        = local.platform_monitoring_workspace_id

  application_type = "web"

  disable_ip_masking   = true
  daily_data_cap_in_gb = 1
  retention_in_days    = 30
  sampling_percentage  = 100

  tags = var.tags
}
