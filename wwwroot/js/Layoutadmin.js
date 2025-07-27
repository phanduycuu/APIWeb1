$.ajax({
    url: "/home/getadmin",
    type: 'get',
    success: function (response) {
        if (response && response.data) {
            const data = response.data
            alert(data.fullname)
        } else {
            console.error("Dữ liệu trả về không hợp lệ.");
        }
    }
})