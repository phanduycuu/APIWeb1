var dataTableEmployer
var dataTableUser
$(document).ready(function () {
    loadDataTableEmployer();
    loadDataTableUser();
});

function loadDataTableEmployer() {
    dataTableEmployer = $('#tbtEmployerData').DataTable({
        "ajax": {
            url: '/Account/GetAllEmployer',
            dataSrc: function (json) {
                console.log(json);  // In ra dữ liệu từ API
                return json.data || [];
            }
        },
        "columns": [
            { data: 'username', "width": "25%", "className": "text-center" },
            { data: 'email', "width": "15%", "className": "text-center" },
            { data: 'fullname', "width": "15%", "className": "text-center" },
            { data: 'companyName', "width": "15%", "className": "text-center" },
            {
                data: 'status',
                "width": "15%",
                "className": "text-center",
                "render": function (data) {
                    if (data === 0) {
                        return "Đang đợi duyệt";
                    } else if (data === 1) {
                        return "Đã duyệt";
                    } else if (data === 2) {
                        return "Từ chối";
                    }
                    return "";
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 d-flex justify-content-center align-items-center gap-1" role=""> 

                <a href="/Account/DetailEmployer?id=${data}" class="btn btn-sm btn-info mx-1">
                    <i class="fa-solid fa-eye"></i> 
                </a>
                
            </div>`;
                },
                "width": "9%"
            }
        ]
    });
}

function loadDataTableUser() {
    dataTableUser = $('#tbtUserData').DataTable({
        "ajax": {
            url: '/Account/GetAllUser',
            dataSrc: function (json) {
                console.log(json);  // In ra dữ liệu từ API
                return json.data || [];
            }
        },
        "columns": [
            { data: 'username', "width": "25%", "className": "text-center" },
            { data: 'email', "width": "15%", "className": "text-center" },
            { data: 'fullname', "width": "15%", "className": "text-center" },
            {
                data: 'status',
                "width": "15%",
                "className": "text-center",
                "render": function (data) {
                    if (data === 0) {
                        return "Đang đợi duyệt";
                    } else if (data === 1) {
                        return "Đã duyệt";
                    } else if (data === 2) {
                        return "Từ chối";
                    }
                    return "";
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 d-flex justify-content-center align-items-center gap-1" role="">
                    <a href="/Account/DetailUser?id=${data}"  class="btn btn-sm btn-info mx-1">
                        <i class="fa-solid fa-eye"></i> 
                    </a>
                    
                    </div>`;
                },
                "width": "9%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'PUT',
                success: function (data) {
                    dataTable.ajax.reload();
                    toast.success(data.message)
                }
            })


        }
    });
}
