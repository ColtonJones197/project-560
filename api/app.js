const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
app.use(bodyParser.json());
app.use(cors());



app.get("/", (req, res) => {
    res.send({something: "hi mom"});
  });
  
app.listen(8081, () => {
console.log("App's running on port 8081");
});