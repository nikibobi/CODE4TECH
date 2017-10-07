let map = L.map('map').setView([43.8551449, 25.9696306], 18);

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1Ijoic2FzNDEiLCJhIjoiY2o4aDg1eW1lMG4wNzMyc3lwZzh5b2kxYSJ9.199CohZXgm_9AiWkNRS5kA'
}).addTo(map);

$.get('/api').done(function (readings) {
    let points = readings.map(r => [r.latitude, r.longitude]);
    L.polyline(points).addTo(map);
});
