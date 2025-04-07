Multitenancy enabled Ecommerce Platform.

To run the project, you need to have the following configurations:
   - Set the connection string to your database.
   - Set the JWT secret key for authentication.
   - Set the Redis connection string for caching.
   - Set the RabbitMQ connection string for message queuing.



To generate migration 

  ex:  add-migration init_tenant -Context TenantDbContext -OutputDir Migrations/Tenants

  Update-databse command Context Specification


  update-database -Context IdentityDbContext
  update-database -Context TenantDbContext
  update-database -Context ApplicationDbContext
