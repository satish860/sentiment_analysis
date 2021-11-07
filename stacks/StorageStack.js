import * as sst from '@serverless-stack/resources'

export default class StorageStack extends sst.Stack {
    
    bucket;

    constructor(scope, id,props) {
        super(scope, id, props);

       this.bucket = new sst.Bucket(this, 'Uploads');
    }
}