workload    = "demo-manager"
environment = "prd"
location    = "uksouth"
instance    = "01"

subscription_id = "845766d6-b73f-49aa-a9f6-eaf27e20b7a8"

storage_account_name = "demomanagerclient"

tags = {
  Environment = "prd"
  Workload    = "demo-manager"
  DeployedBy  = "GitHub-Terraform"
  Git         = "https://github.com/frasermolyneux/demo-manager"
}
