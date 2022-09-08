
import axios from "axios";
export default {

    data() {
        return {

            name: "",
            email: "",

        };
    },
    created() {
        //user is not authorized
        if (localStorage.getItem("token") === null) {
            this.$router.push("/login");
        }
    }, mounted() {
        axios
            .get("http://localhost:5000/api/auth/user", {
                headers: {
                    Authorization: 'Bearer' + token,
                    token: localStorage.getItem("token")
                }
            })
            .then(res => {
                console.log(res)
                this.name = res.data.foundUser.name;
                this.email = res.data.foundUser.email;
            });
    },


};