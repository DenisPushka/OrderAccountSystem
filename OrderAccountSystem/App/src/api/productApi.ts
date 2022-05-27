const URL = 'api/v1/product'

function Get(id: Guid): Promise<ProductTS> {
    return fetch(`${URL}/${id}`).then(x => x.json());
}

function GetArray(): Promise<ProductTS[]> {
    return fetch(URL)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.statusText)
            }
            return response.json()
        })
}
// реакт роутер
function Add(productTS: ProductTS): Promise<void> {
    return fetch(URL + "/add", {
        method: 'POST',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(productTS)
    }).then();
}

function Delete(id: Guid): Promise<void> {
    return fetch(`${URL + "/delete"}`, {
        method: 'DELETE',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(id)
    }).then();
}

export const productApi = {
    getArray: GetArray,
    getProduct: Get,
    postProduct: Add,
    deleteProduct: Delete,
} as const;

export interface ProductTS {
    id: Guid;
    name: string;
    price: number;
    description: string;
    isStock: boolean;
    count: number;
}
