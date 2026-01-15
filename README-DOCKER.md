# JobMatix PostgreSQL Docker Setup

Quick guide for setting up the PostgreSQL development environment using Docker.

## Prerequisites

- Docker installed and running
- Docker Compose installed (usually comes with Docker Desktop)

## Quick Start

### 1. Start the PostgreSQL Server

```bash
# From the JobMatix root directory
docker-compose up -d
```

This will start:
- **PostgreSQL Server** on port `5432`
- **pgAdmin** (web UI) on port `5050` at http://localhost:5050

### 2. Verify It's Running

```bash
# Check container status
docker-compose ps

# View logs
docker-compose logs postgres
docker-compose logs pgadmin

# Follow logs in real-time
docker-compose logs -f postgres
```

### 3. Access the Database

**Option A: Using psql (command line)**
```bash
# Connect to main database
docker-compose exec postgres psql -U jobmatix_user -d jobmatix_main

# Connect to jobs database
docker-compose exec postgres psql -U jobmatix_user -d jobmatix_jobs

# Connect to POS database
docker-compose exec postgres psql -U jobmatix_user -d jobmatix_pos
```

**Option B: Using pgAdmin (web interface)**
1. Open browser to http://localhost:5050
2. Login with:
   - Email: `admin@jobmatix.local`
   - Password: `admin`
3. Add server connection:
   - Right-click "Servers" → "Register" → "Server"
   - General tab:
     - Name: `JobMatix Local`
   - Connection tab:
     - Host: `postgres` (or `localhost` if connecting from host machine)
     - Port: `5432`
     - Username: `jobmatix_user`
     - Password: `JobMatix2026!Dev`
     - Save password: ✓

**Option C: Using any PostgreSQL client**
```
Host: localhost
Port: 5432
Username: jobmatix_user
Password: JobMatix2026!Dev
Databases: jobmatix_main, jobmatix_jobs, jobmatix_pos, jobmatix_backup
```

## Database Information

Four databases are automatically created:

| Database | Purpose |
|----------|---------|
| `jobmatix_main` | Main application database |
| `jobmatix_jobs` | Job tracking module |
| `jobmatix_pos` | Point of Sale system |
| `jobmatix_backup` | Backup agent data |

## Common Commands

```bash
# Start services
docker-compose up -d

# Stop services
docker-compose down

# Stop and remove all data (CAREFUL!)
docker-compose down -v

# Restart services
docker-compose restart

# View logs
docker-compose logs -f postgres

# Execute SQL file
docker-compose exec -T postgres psql -U jobmatix_user -d jobmatix_jobs < script.sql

# Backup database
docker-compose exec -T postgres pg_dump -U jobmatix_user jobmatix_jobs > backup.sql

# Restore database
docker-compose exec -T postgres psql -U jobmatix_user -d jobmatix_jobs < backup.sql

# Open PostgreSQL shell
docker-compose exec postgres bash
```

## Testing the Connection from .NET

Connection string format:
```
Host=localhost;Port=5432;Database=jobmatix_jobs;Username=jobmatix_user;Password=JobMatix2026!Dev;Pooling=true;Maximum Pool Size=20;
```

Test connection code (VB.NET with Npgsql):
```vb
Imports Npgsql

Dim connString As String = "Host=localhost;Port=5432;Database=jobmatix_main;Username=jobmatix_user;Password=JobMatix2026!Dev"
Using conn As New NpgsqlConnection(connString)
    conn.Open()
    Console.WriteLine("Connected successfully!")
    Console.WriteLine("PostgreSQL version: " & conn.PostgreSqlVersion.ToString())
End Using
```

## Troubleshooting

### Container won't start
```bash
# Check if port 5432 is already in use
sudo lsof -i :5432

# View detailed logs
docker-compose logs postgres
```

### Can't connect to database
```bash
# Verify container is healthy
docker-compose ps

# Check PostgreSQL logs
docker-compose logs postgres | tail -50

# Test connection from inside container
docker-compose exec postgres psql -U jobmatix_user -d jobmatix_main -c "SELECT version();"
```

### Reset everything
```bash
# Stop and remove containers, networks, and volumes
docker-compose down -v

# Start fresh
docker-compose up -d

# Wait for initialization
sleep 10

# Verify
docker-compose exec postgres psql -U jobmatix_user -d jobmatix_main -c "\l"
```

## Data Persistence

- Database data is stored in Docker volume `postgres_data`
- pgAdmin settings stored in `pgadmin_data`
- Data persists even when containers are stopped
- Use `docker-compose down -v` to remove volumes (deletes all data!)

## Security Notes

**⚠️ Development Environment Only**

The credentials in this setup are for development only:
- Default passwords are simple and documented
- PostgreSQL is exposed on all interfaces
- pgAdmin has minimal security

**For production:**
- Change all passwords to strong, unique values
- Use environment variables or secrets management
- Enable SSL/TLS connections
- Restrict network access
- Enable PostgreSQL authentication logging
- Regular backups

## Performance Tuning

Edit `docker-compose.yml` to add PostgreSQL configuration:

```yaml
postgres:
  command: 
    - "postgres"
    - "-c"
    - "shared_buffers=256MB"
    - "-c"
    - "max_connections=100"
    - "-c"
    - "work_mem=16MB"
```

Or create `docker/postgres/postgresql.conf` and mount it.

## Next Steps

1. ✅ Start Docker containers
2. ✅ Verify connection using pgAdmin
3. ✅ Test connection from .NET application
4. 📝 Begin Phase 1 of migration (see POSTGRESQL_MIGRATION_GUIDE.md)

## Additional Resources

- [PostgreSQL Docker Official Image](https://hub.docker.com/_/postgres)
- [pgAdmin Docker Image](https://www.pgadmin.org/docs/pgadmin4/latest/container_deployment.html)
- [Npgsql Documentation](https://www.npgsql.org/doc/)
