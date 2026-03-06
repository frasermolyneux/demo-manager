data "azurerm_resource_group" "rg" {
  name = local.workload_resource_group.name
}

removed {
  from = azurerm_resource_group.default
  lifecycle {
    destroy = false
  }
}
