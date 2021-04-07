const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const soundSchema = new Schema({
	_id: Number,
    scene: Number,
    date: String,
    frequency: Number,
    spheres: [String]
}, {
    versionKey: false, // You should be aware of the outcome after set to false
    _id: false
});

const Sound = mongoose.model('Sound', soundSchema);

module.exports= {Sound};

    // spheres: [{key: Number, xcoord: Number, ycoord: Number, zcoord: Number}]
    // emitted: {type: Date, default: Date.now},
    // frequency: { type: Number, min: 20, max: 20000 },
    // // spheres: [{key: Number, xcoord: Number, ycoord: Number, zcoord: Number}]
    // spheres: [{key: Number, coordinates: [[Number]]}]