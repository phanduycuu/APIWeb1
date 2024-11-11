var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/jobseeker/getall' },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'userName', "width": "10%" },        
            { data: 'email', "width": "15%" },
            { data: 'fullName', "width": "15%" },
            { data: 'phone', "width": "10%" },
            { data: 'create_at', "width": "10%" },
            { data: 'update_at', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                    <a href="/admin/jobseeker/update/${data}" class="btn btn-sm btn-warning" > <i class="fa-solid fa-pen-to-square"></i></a>
                    <a onClick=Delete('/admin/jobseeker/delete/${data}') class="btn btn-sm btn-danger" > <i class="fa-solid fa-trash"></i></a>`
                },
                "width": "20%"
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
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
