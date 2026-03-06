workload    = "demo-manager"
environment = "prd"
location    = "uksouth"

subscription_id = "845766d6-b73f-49aa-a9f6-eaf27e20b7a8"

storage_account_name = "demomanagerclient"

tags = {
  Environment = "prd"
  Workload    = "demo-manager"
  DeployedBy  = "GitHub-Terraform"
  Git         = "https://github.com/frasermolyneux/demo-manager"
}

platform_monitoring_state = {
  resource_group_name  = "rg-tf-platform-monitoring-prd-uksouth-01"
  storage_account_name = "sa74f04c5f984e"
  container_name       = "tfstate"
  key                  = "terraform.tfstate"
  subscription_id      = "7760848c-794d-4a19-8cb2-52f71a21ac2b"
  tenant_id            = "e56a6947-bb9a-4a6e-846a-1f118d1c3a14"
}

platform_workloads_state = {
  resource_group_name  = "rg-tf-platform-workloads-prd-uksouth-01"
  storage_account_name = "sadz9ita659lj9xb3"
  container_name       = "tfstate"
  key                  = "terraform.tfstate"
  subscription_id      = "7760848c-794d-4a19-8cb2-52f71a21ac2b"
  tenant_id            = "e56a6947-bb9a-4a6e-846a-1f118d1c3a14"
}
