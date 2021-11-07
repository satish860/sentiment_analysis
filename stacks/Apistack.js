import * as sst from "@serverless-stack/resources";

export default class Apistack extends sst.Stack {

  api;

  constructor(scope, id, props) {
    super(scope, id, props);

    const {bucket} = props;

    // Create a HTTP API
    this.api = new sst.Api(this, "Api", {
      defaultFunctionProps: {
        srcPath: "src/Api",
        environment: {
          BUCKET: bucket.bucketName,
          REGION: this.region
        }
      },
      routes: {
        "GET /": "Api::Api.Handlers::Handler",
        "GET /presigned": "Api::Api.PresignedHandler::Handler",
      }
    });
    
    this.api.attachPermissions([bucket]);

    // Show the endpoint in the output
    this.addOutputs({
      "ApiEndpoint": this.api.url,
    });
  }
}
