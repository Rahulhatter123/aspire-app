var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Clean_Architecture_Sample_API>("api")
    .WithExternalHttpEndpoints();
    
builder.Build().Run();

//COMMENTS ADDED UPDATED