const express = require('express');
const app = express();
const port = 4000;
const dogComponent = require('./components/dogs');
const bodyParser = require('body-parser');
const apiKeyDemo = require('./components/apiKeyDemo');
const cors = require('cors');
const db = require('./db');

const customHeaderCheckerMiddleware = function(req, res, next) {
    console.log('Middleware is active!');
    if(req.headers['custom-header-param'] === undefined)
    {
        return res.status(400).json({ reason: "custom-header-param header missing"});
    }

    // pass the control to the next handler in line
    next();
}

//app.use(customHeaderCheckerMiddleware);
app.use(bodyParser.json());
app.use(cors())

saveToPlay = 0;
lastSave = 0;
buttonBool = 0;
waitTimer = 0;
playOn = 0;
ID = 0;

app.post('/pc', function(req, res) {
	if(req.body.button >= 0 && req.body.button != 5){
		db.query('INSERT INTO pcOut(speedControl, turnControl, button, repeatControl) VALUES (?,?,?,?)', [req.body.speedControl, req.body.turnControl, req.body.button, req.body.repeatControl]);
	        console.log('\n');
		console.log('ID: ');
		console.log(ID);
        	console.log('button: ');
        	console.log(req.body.button);
        	console.log('repeatCtrl');
        	console.log(req.body.repeatControl);
        	console.log('butBo: ');
        	console.log(buttonBool);
        	console.log('playOn: ');
        	console.log(playOn);
		console.log('waitTimer: ');
		console.log(waitTimer);
        	console.log('\n');
		ID = ID + 1;
		if(waitTimer != 0)
		{
			waitTimer = waitTimer - 1;
		}
	}
	if(req.body.button == 2 && req.body.repeatControl == 3)
	{
		if(playOn == 0)
		{
			if(buttonBool == 0)
			{
				db.query('SET SQL_SAFE_UPDATES = 0');
				db.query('DELETE FROM recordedInputs');
				db.query('ALTER TABLE recordedInputs AUTO_INCREMENT = 1');
				buttonBool = 1;
			}

			if(buttonBool == 1)
			{
				db.query('INSERT INTO recordedInputs(speedControl, turnControl) VALUES (?,?)', [req.body.speedControl, req.body.turnControl]);
				console.log('\nrecording...\n');
				console.log('buttonBool: ');
				console.log(buttonBool);
			}
		}
	}
	if(req.body.button == 3 && waitTimer == 0)
	{
		if(buttonBool == 1 && playOn == 0)
		{
			buttonBool = 0;
			waitTimer = 10;
			console.log('\nrecording stopped\n');
			console.log('\nbuttonBool: ');
			console.log(buttonBool);
			console.log('playOn: ');
			console.log(playOn);
		}
		else if ( buttonBool == 0 && playOn == 0 && req.body.repeatControl == 1)
		{
			playOn = 1;
			console.log('\nplayingRecor\n');
			console.log('playOn: ');
			console.log(playOn);
			waitTimer = 10;
		}
		else if (buttonBool == 0 && playOn == 1 && req.body.repeatControl == 2)
		{
			saveToPlay = lastSave;
			playOn = 0;
			console.log('\nplaingStopped\n');
			console.log('buttonBool: ');
			console.log(buttonBool);
			console.log('playOn: ');
			console.log(playOn);
			waitTimer = 10;
		}
	}
	if(req.body.button == 4)
	{
		db.query('UPDATE pcOut SET connectionCheck=1 ORDER BY idpcOut DESC LIMIT 1');
	}
	if(req.body.button == 5)
	{
		setTimeout(() => {  console.log("---!"); }, 500);
		db.query('SET SQL_SAFE_UPDATES = 0');
		db.query('DELETE FROM pcOut');
		db.query('DELETE FROM rbOut');
		db.query('ALTER TABLE pcOut AUTO_INCREMENT = 1');
		db.query('ALTER TABLE rbOut AUTO_INCREMENT = 1');
		setTimeout(() => {  console.log("Reset!"); }, 500);
		console.log('\ndeleted pcOut and rbOut\n');
		db.query('INSERT INTO pcOut(idpcOut, speedControl, turnControl, button) VALUES (?,?,?,?)', [1, 0, 0, 0]);
		db.query('INSERT INTO rbOut(idrbOut, Deg, Speed) VALUES (?,?,?)', [1, 0, 0]);
	}
	if(playOn == 1 && saveToPlay <= lastSave)
	{
		console.log(buttonBool);
		console.log(saveToPlay);
		console.log(lastSave);
	}
	res.sendStatus(201);
});

app.get('/lastsave', (req, res) => {
	db.query('SELECT idrecordedInputs FROM recordedInputs ORDER BY idrecordedInputs DESC LIMIT 1')
	.then(result => {
		res.json(result);
		lastSave = result[0].idrecordedInputs;
		db.query('SET SQL_SAFE_UPDATES = 0');
		db.query('DELETE FROM rbOut');
		db.query('ALTER TABLE rbOut AUTO_INCREMENT = 1');
	});
});
app.get('/firstSave', (req, res) => {
	db.query('SELECT idrecordedInputs FROM recordedInputs ORDER BY idrecordedInputs ASC LIMIT 1')
	.then(result => {
		res.json(result);
		saveToPlay = result[0].idrecordedInputs;
		console.log("f save:");
		console.log(saveToPlay);
		console.log(result);
	});
});
app.post('/rb', function(req, res) {
	db.query('INSERT INTO rbOut(Deg, Speed) VALUES (?,?)', [req.body.Deg, req.body.Speed]);
	res.sendStatus(201);
});
app.get('/pc', (req, res) => {
	console.log('\n/pc [GET] used');
	db.query('SELECT * FROM pcOut')
	.then(result => {
		res.json(result);
	});
});
app.get('/pc/last', (req, res) => {
	db.query('SELECT * FROM pcOut ORDER BY idpcOut DESC LIMIT 1')
	.then(result => {
		res.json(result);
	})
	.catch(() => {
        	res.json('wait');
	});
});
app.get('/pc/:id', (req, res) => {
	db.query('SELECT * FROM pcOut WHERE idpcOut = ?',[req.params.id])
	.then(results => {
		res.json(results);
	});
});

app.get('/rb', (req, res) => {
	db.query('SELECT * FROM rbOut ORDER BY idrbOut DESC LIMIT 1')
	.then(result => {
		res.json(result);
	});
});

app.put('/rb/put', (req, res) => {
	db.query('UPDATE pcOut SET connectionCheck=1 ORDER BY idpcOut DESC LIMIT 1')
	.then(results => {
		res.json('check');
	});
});
app.get('/rb/check', (req, res) => {
	db.query('SELECT connectionCheck FROM pcOut ORDER BY idpcOut DESC LIMIT 1')
	.then(result => {
		res.json(result);
	})
	.catch(() => {
		res.json('deleted')
	});
});
app.get('/save', (req, res) => {
	db.query('SELECT * FROM recordedInputs WHERE idrecordedInputs = ?', saveToPlay)
	.then(result => {
		res.json(result);
	})
	.catch(() => {
		res.json('fin');
	});
	saveToPlay = saveToPlay + 1;
});
app.get('/palystop', (req, res) => {
	if(saveToPlay < (lastSave - 1))
	{
		res.json(0);
	}
	else
	{
		res.json(1);
	}
});


/* basic HTTP method handling */
/*app.get('/hello', (req, res) => res.send('Hello GET World!'));
app.post('/hello', (req, res) => res.send('Hello POST World!'));
app.put('/hello', (req, res) => res.send('Hello PUT World!'));
app.delete('/hello', (req, res) => res.send('Hello DELETE World!'));
*/
/* Route parameters */
/*app.get('/hello/:parameter1/world/:parameter2', (req, res) => {
    res.send('Your route parameters are\n' + JSON.stringify(req.params));
});
*/
/* Example of defining routes with different method handlers */
/*app.route('/world')
    .get((req,res) => res.send('get World'))
    .post((req, res) => res.send('post World'))
    .put((req, res) => res.send('put World'))
    .delete((req, res) => res.send('delete World'))
*/
/* demonstrate route module/component usage - the dogComponent content is defined in separate file */
/*app.use('/dogs', dogComponent);

app.use('/apiKey', apiKeyDemo);
*/

/* DB init */
Promise.all(
    [
        db.query(`CREATE TABLE IF NOT EXISTS dogHouse(
            id INT AUTO_INCREMENT PRIMARY KEY,
            name VARCHAR(32),
            image VARCHAR(256)
        )`)
        // Add more table create statements if you need more tables
    ]
).then(() => {
    console.log('database initialized');
    app.listen(port, () => {
        console.log(`Example API listening on http://localhost:${port}\n`);
        console.log('Available API endpoints');
/*        console.log('  /hello [GET, POST, PUT, DELETE]');
        console.log('  /hello/{param1}/world/{param2} [GET]');
        console.log('  /world [GET, POST, PUT, DELETE]');
        console.log('\n  /dogs [GET, POST]');
        console.log('  /dogs/{dogId} [GET, DELETE]');
        console.log('\n  /apikey/new/{username} [GET]');
*/        console.log('  /apikey/protected} [GET]');
        console.log('\n\n Use for example curl or Postman tools to send HTTP requests to the endpoints');
	console.log('\n\n /pc [POST][GET]');
	console.log('\n\n /pc/last [GET] ORDER Last id');
	console.log('\n\n /pc/:id [GET] id parameter');
	console.log('\n\n /rb [POST]');
	console.log('\n\n /rb/put [GET] put to pcOut');
	console.log('\n\n /rb/check [get] pcOut Check)');
   });
});
