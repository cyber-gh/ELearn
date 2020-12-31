import AWS from "aws-sdk";

AWS.config.update({
    accessKeyId: process.env.REACT_APP_AWS_ACCESS_KEY,
    secretAccessKey: process.env.REACT_APP_AWS_SECRET_KEY
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