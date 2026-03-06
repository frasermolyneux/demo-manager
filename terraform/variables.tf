variable "workload" {
  default = "demo-manager"
}

variable "environment" {
  default = "prd"
}

variable "location" {
  default = "uksouth"
}

variable "instance" {
  default = "01"
}

variable "subscription_id" {}

variable "storage_account_name" {
  description = "Name of the storage account for ClickOnce artifact hosting."
}

variable "tags" {
  default = {}
}
