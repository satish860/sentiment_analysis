import Apistack from "./Apistack";
import StorageStack from "./StorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  

  const storage = new StorageStack(app,"storage-stack");

  new Apistack(app, "api-stack",{
    bucket: storage.bucket
  });

  // Add more stacks
}
