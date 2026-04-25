variable "workload_name" {
  default = "demo-manager"
}

variable "environment" {
  default = "prd"
}

variable "location" {
  default = "uksouth"
}

variable "subscription_id" {}

variable "storage_account_name" {
  description = "Name of the storage account for ClickOnce artifact hosting."
}

variable "tags" {
  default = {}
}

variable "platform_monitoring_state" {
  description = "Backend config for platform-monitoring remote state."
  type = object({
    resource_group_name  = string
    storage_account_name = string
    container_name       = string
    key                  = string
    subscription_id      = string
    tenant_id            = string
  })
}

variable "platform_workloads_state" {
  description = "Backend config for platform-workloads remote state."
  type = object({
    resource_group_name  = string
    storage_account_name = string
    container_name       = string
    key                  = string
    subscription_id      = string
    tenant_id            = string
  })
}
