locals {
  workload_resource_groups = {
    for location in [var.location] :
    location => data.terraform_remote_state.platform_workloads.outputs.workload_resource_groups[var.workload][var.environment].resource_groups[lower(location)]
  }

  workload_resource_group = local.workload_resource_groups[var.location]

  platform_monitoring_workspace_id = data.terraform_remote_state.platform_monitoring.outputs.log_analytics.id
}
