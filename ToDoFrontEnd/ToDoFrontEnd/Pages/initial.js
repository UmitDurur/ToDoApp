var app = new Vue({
    el: '#app',
    data: {
        message: "Hello Vue"
    },
    methods: {
        login: function () {
            fetch('https://localhost:44352/connect/token')
                .then((response) => response.json())
                .then((data) => console.log(data));
        },
        getTitles: function () {
            fetch('https://localhost:44352/api/app/to-do/titles')
                .then((response) => response.json())
                .then((data) => console.log(data));
        },
        permissions: function () {
            return abp.currentUser;
        }
    }

});