
global using NLog.Extensions.Logging;
global using Quartz;
global using Quartz_HealthCheck_NLog_Worker.Extensions;
global using Quartz_HealthCheck_NLog_Worker.Jobs;
global using Microsoft.AspNetCore.Hosting;
global using MediatR;
global using System.Reflection;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Builder;
global using Quartz_HealthCheck_NLog_Worker.HealthChecks;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Quartz_HealthCheck_NLog_Worker.Events.HealthCheck;
global using Quartz_HealthCheck_NLog_Worker.Common;