var MongoClient = require('mongodb').MongoClient;
const client = new MongoClient('mongodb://localhost');
const connection = client.connect();

module.exports = {client, connection};

