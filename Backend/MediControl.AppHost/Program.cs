var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("medi-db-volume") 
    .AddDatabase("DefaultConnection");

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithReference(postgres)
    .WithHttpEndpoint(name: "api")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("frontend", "../../Frontend/vite")
    .WithReference(backend)
    .WithHttpEndpoint(env: "PORT") 
    .WithExternalHttpEndpoints();

builder.Build().Run();