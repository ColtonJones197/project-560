const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
require('dotenv').config();
//console.log(process.env);

const app = express();
app.use(bodyParser.json());
app.use(cors());

// const Connection = require('tedious').Connection
// const Request = require('tedious').Request
// console.log(`Connecting to ${process.env.DB_HOST}`);
// const config = {
//   server: process.env.DB_HOST,
//   authentication: {
//     type: 'default',
//     options: {
//       userName: process.env.DB_USER, // update me
//       password: process.env.DB_PWD // update me
//     }
//   },
//   options: {
//     encrypt: true,
//     database: process.env.DB_NAME
//   }
// }

// const connection = new Connection(config)

// connection.on('connect', (err) => {
//   if (err) {
//     console.log(err)
//   } else {
//     executeStatement()
//   }
// });

// connection.connect();

// function executeStatement () {
//   request = new Request("select 123, 'hello world'", (err, rowCount) => {
//     if (err) {
//       console.log(err)
//     } else {
//       console.log(`${rowCount} rows`)
//     }
//     connection.close()
//   })

//   request.on('row', (columns) => {
//     columns.forEach((column) => {
//       if (column.value === null) {
//         console.log('NULL')
//       } else {
//         console.log(column.value)
//       }
//     })
//   })

//   connection.execSql(request)
// }

async function attemptConnection() {
    try{
        console.log("trying!");
        await sql.connect("Server=DESKTOP-VT4KSCJ\\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False");
        const result = await sql.query('SELECT Username, Avatar FROM Chesscom.Player');
        console.dir(result);
    } catch(err) {
        console.log("Connection failed");
        console.log(err);
    }
}
  
app.listen(8081, () => {
console.log("App's running on port 8081");
  attemptConnection();
});