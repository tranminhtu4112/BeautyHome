//javascript.js
//set map options
var myLatLng = { lat: 10.762622, lng: 106.660172 };
var mapOptions = {
    center: myLatLng,
    zoom: 7,
    mapTypeId: google.maps.MapTypeId.ROADMAP

};

//Tạo bản đồ
var map = new google.maps.Map(document.getElementById('googleMap'), mapOptions);

//Tạo một đối tượng DirectionsService để sử dụng phương thức route và nhận kết quả cho yêu cầu của chúng tôi
var directionsService = new google.maps.DirectionsService();

//Tạo một đối tượng DirectionsRenderer mà chúng ta sẽ sử dụng để hiển thị route
var directionsDisplay = new google.maps.DirectionsRenderer();

//Liên kết DirectionsRenderer với bản đồ
directionsDisplay.setMap(map);


//Xác định hàm calcRoute
function calcRoute() {
    //Tạo yêu cầu
    var request = {
        origin: document.getElementById("from").value,
        destination: document.getElementById("to").value,
        travelMode: google.maps.TravelMode.DRIVING, //WALKING, BYCYCLING, TRANSIT
        unitSystem: google.maps.UnitSystem.IMPERIAL
    }

    //Chuyển yêu cầu đến phương thức tuyến đường
    directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {

            //Xác nhận khoảng cách và thời gian
            const output = document.querySelector('#output');
            output.innerHTML = "<div class='alert-info'>Từ: " + document.getElementById("from").value + ".<br />Đến: " + document.getElementById("to").value + ".<br /> Đoạn đường phải đi <i class='fas fa-road'></i> : " + result.routes[0].legs[0].distance.text + ".<br />Thời gian <i class='fas fa-hourglass-start'></i> : " + result.routes[0].legs[0].duration.text + ".</div>";

            //Lộ trình hiển thị
            directionsDisplay.setDirections(result);
        } else {
            //Xóa tuyến đường khỏi bản đồ
            directionsDisplay.setDirections({ routes: [] });
            //Bản đồ trung tâm ở HCM
            map.setCenter(myLatLng);

            //Hiển thị thông báo lỗi
            output.innerHTML = "<div class='alert-danger'><i class='fas fa-exclamation-triangle'></i> Could not retrieve driving distance.</div>";
        }
    });

}



//Tạo đối tượng tự động hoàn tất cho tất cả các đầu vào
var options = {
    types: ['(cities)']
}

var input1 = document.getElementById("from");
var autocomplete1 = new google.maps.places.Autocomplete(input1, options);

var input2 = document.getElementById("to");
var autocomplete2 = new google.maps.places.Autocomplete(input2, options);
