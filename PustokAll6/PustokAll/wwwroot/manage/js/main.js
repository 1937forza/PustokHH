$(function () {

    $(document).on("click", ".delete-btn", function (event) {
        event.preventDefault();
        var $this = $(this);

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
                fetch($this.attr("href"), { method: "post" })
                    .then(response => {
                        window.location.reload();
                    })
            }
        })
    })



})