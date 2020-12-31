import AWS from "aws-sdk";

AWS.config.update({
    accessKeyId: "AKIARJNTQGZVGKTNOK2R",
    secretAccessKey: "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"
})

const bucketName = "cybergh";

const bucket = new AWS.S3({
    params: {Bucket: bucketName},
    region: "us-east-1"
})

const uploadFile = async (file, progressCallback) => {
    const params = {
        ACL: 'public-read',
        Key: file.name,
        ContentType: file.type,
        Body: file,
        Bucket: bucketName
    }
    let ans = await bucket.putObject(params)
        .on('httpUploadProgress', (evt) => {
            // console.log(evt.loaded / evt.total)
            progressCallback(evt.loaded / evt.total * 100)
        })
        .promise()

    return `https://${bucketName}.s3.amazonaws.com/${file.name}`;

}

export {bucket, uploadFile};