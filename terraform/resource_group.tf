resource "azurerm_resource_group" "default" {
  name     = "rg-${var.workload}-${var.environment}-${var.location}-${var.instance}"
  location = var.location
  tags     = var.tags
}

import {
  to = azurerm_resource_group.default
  id = "/subscriptions/${var.subscription_id}/resourceGroups/rg-${var.workload}-${var.environment}-${var.location}-${var.instance}"
}
