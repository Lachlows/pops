
const express = require('express');
var app =require('express')();// express();
var http = require('http').Server(app);
var io = require('socket.io')(http);


app.use(express.static('public'));
var count = 0;

app.get('/', function(req, res){
	count++;
	res.sendFile(__dirname + '/index.html');
	//
});

app.get('/0', function(req, res){
	
	res.sendFile(__dirname + '/index0.html');
	//
});

app.get('/1', function(req, res){
	
	res.sendFile(__dirname + '/index1.html');
	//
});

app.get('/jquery', function(req, res){
	
	res.sendFile(__dirname + '/public/jquery.min.js');
	//
});

app.get('/css', function(req, res){
	
	res.sendFile(__dirname + '/public/index.css');
	//
});

app.get('/logo', function(req, res){
	
	res.sendFile(__dirname + '/public/images/logo_pops_V2.png');
	//
});

app.get('/popcorn', function(req, res){
	
	res.sendFile(__dirname + '/public/images/popcorn.png');
	//
});

app.get('/back-red', function(req, res){
	
	res.sendFile(__dirname + '/public/images/back-red.png');
	//
});

app.get('/marvinwoff2', function(req, res){
	
	res.sendFile(__dirname + '/public/fonts/marvin-regular-webfont.woff2');
	//
});

app.get('/marvinwoff', function(req, res){
	
	res.sendFile(__dirname + '/public/fonts/marvin-regular-webfont.woff');
	//
});

/*app.get('/lilita', function(req, res){
	
	res.sendFile(__dirname + '/public/fonts/LilitaOne-Regular.ttf');
	//
});*/



var userId = 0;

io.on('connection', function(socket){
  socket.userId = userId ++;
  console.log('a user connected, user id: ' + socket.userId);

  socket.on('spawn', function(msg){
		//msg = JSON.parse(msg);
		console.log('message from user ' + msg.pseudo);
		io.emit("spawn",msg);
  });
  
  socket.on('input1', function(msg){
		console.log(msg);
		//msg = JSON.parse(msg);
		console.log("a "+msg.pseudo+" user has pressed input1");
		io.emit("input1",msg);
  });
  
  socket.on('input2', function(msg){
		//msg = JSON.parse(msg);
		console.log('user ' + msg.pseudo+" has pressed input2");
		io.emit("input2",msg);
  });

    socket.on('accelerometer', function(msg){
		//msg = JSON.parse(msg);
		console.log('accelerometer ' + msg.x);
		io.emit("input2",msg);
  });

  
});


http.listen(3000, function(){
	console.log('listening on *:3000');
});