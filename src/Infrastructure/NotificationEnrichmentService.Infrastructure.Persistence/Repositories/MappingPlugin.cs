using Itmo.Dev.Platform.Persistence.Postgres.Plugins;
using Npgsql;

namespace NotificationEnrichmentService.Infrastructure.Persistence.Repositories;

public class MappingPlugin : IPostgresDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder dataSource) { }
}