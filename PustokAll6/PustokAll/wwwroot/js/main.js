$(function () {

    $(document).on("click", ".single-eye-btn", function () {
        console.log("ClickMe")

        let id = $(this).attr("data-id");

        fetch(`/home/getbook/${id}`)
            //.then(response => response.json())
            .then(response => response.text())
            .then(data => {

                console.log(data)
                $("#detailModal .modal-content").html(data)
                /*  $("#detailModal").find(".product-title").text(data.name)*/
            })



        $("#detailModal").modal("show")
    })

    $(document).on("click", ".add-btn", function (event) {
        event.preventDefault();
        console.log("Click addBnt")

        let url = $(this).attr("href");
        $("#cards-products").html("");
        let cardProducts = "";

        fetch(url)
            .then(response => response.json())
            .then(data => {
                data.forEach(book => {
                    console.log(book)
                    cardProducts += `
                            <div class="cart-product">
                                <a href="product-details.html" class="image">
                                    <img src="image/products/${book.image}" alt="">
                                </a>
                                <div class="content">
                                    <h3 class="title">
                                            <a href="product-details.html">
                                                   ${book.name}
                                            </a>
                                    </h3>
                                    <p class="price"><span class="qty">${book.count} ×</span> ${book.price}</p>
                                    <button class="cross-btn" data-id="${book.id}"><i class="fas fa-times"></i></button>
                                </div>
                            </div>
                    `;
                });

                $("#cards-products").html(cardProducts);
            });

        console.log(url);
            
    })


    $(document).on("click", ".cross-btn", function (event) {
        event.preventDefault();
        let $this = $(this);
        let bookId = $this.attr("data-id");
        alert(bookId)
        fetch(`/book/removebasket?bookid=${bookId}`).then(response => {
            $this.parents(".cart-product").remove();
        })
    })
    
})