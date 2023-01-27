var dataTable
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns":[
            { "data":"title","width":"15%"},
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "coverType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="btn-group w-75 ">
                              <a class="btn btn-primary" href="/Admin/Product/Upsert?id=${data}">
                                 <i class="bi bi-pen"></i> &nbsp; Edit
                              </a>
                                    &nbsp;
                              <a class="btn btn-danger" onClick=Delete('/Admin/Product/Delete/${data}')   >
                                <i class="bi bi-x-circle"></i> &nbsp; Delete
                              </a>
                            </div> `
                }, "width": "20%"
            },
        ]
    })

}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) { 
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
                else {
                    toastr.error(data.message);
                   }
             }
            })

        }
    })

}