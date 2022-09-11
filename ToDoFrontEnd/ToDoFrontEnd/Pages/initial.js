var app = new Vue({
    el: '#app',
    data: {
        message: "Hello Vue",
        accessToken: "",
    },
    methods: {
        saveToken: function () {
            this.accessToken = "Bearer Token";
        },
        login: function () {
            fetch('https://localhost:44352/connect/token')
                .then((response) => response.json())
                .then((data) => console.log(data));
        },
        getTitles: function () {
            fetch('https://localhost:44352/api/app/to-do/titles', {
                headers: { Authentication: "Bearer " + this.$cookies.get("Access-Token") }
            })
                .then(resp => resp.json())
                .then(json => console.log(json))
        },
        permissions: function () {
            return abp.currentUser;
        }
    }

});