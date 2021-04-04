// cd C:\Program Files\MongoDB\Server\4.4\bin
// mongod.exe --dbpath "C:\data"
// mongo.exe

// SERVER

const server = new (require('koa'))();
const {router} = require('./router');

server.use(require('koa-body')({
    multipart: true,
    formLimit: "10mb",
    jsonLimit: "50mb",
    textLimit: "10mb",
    enableTypes: ['json', 'form', 'text']
}));

server.use(require('koa-logger')())
server.use(require('koa-bodyparser')());
server.use(router.routes());

// CONNECT TO DATABASE

const mongoose = require('mongoose');
const db_URI = 'mongodb://mongo:27017/apiApp12';
const db_update = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
    useFindAndModify: false
};

mongoose.connect(db_URI, db_update).then(()=>{
    console.log('listening on port 3000')
    server.listen(3000);
});

