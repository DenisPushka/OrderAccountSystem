import React from 'react';
import styles from "./App.scss";
import {ProductTS, productApi} from "../api/productApi";

export const App: React.FC = ({}) => {
    const [productTS, setProductTS] = React.useState<ReadonlyArray<ProductTS>>([]);
    const [name, setName] = React.useState('');
    const [price, setPrice] = React.useState(0);
    const [count, setCount] = React.useState(0);
    const [isStock, setIsStock] = React.useState(true);
    const [description, setDescription] = React.useState('');
    const [id, setId] = React.useState();

    React.useEffect(() => {
        productApi.getArray().then(setProductTS);
    }, []);

    const onCreate = () => {
        const products: ProductTS = {
            id: undefined,
            name: name,
            price: price,
            description: description,
            isStock: isStock,
            count: count
        };
        productApi.postProduct(products);
        setProductTS([...productTS, products]);
    };

    const deleteProduct = () => {
        productApi.deleteProduct(id);
        setProductTS([...productTS]);
    }
 // b4daae84-d71a-420d-9958-4817630f7bd0
    console.log(productTS)
    return (
        <div className={styles.root}>
            <p>ADMIN</p>

            <div className={styles.data}>
                <label>Название: </label>
                <input value={name} onChange={e => setName(e.target.value)}/>
                <label>Цена</label>
                <input value={price} onChange={e => setPrice(+e.target.value)}/>
                <label>Количество</label>
                <input value={count} onChange={e => setCount(+e.target.value)}/>
                <label>Описание товара: </label>
                <input value={description} onChange={e => setDescription(e.target.value)}/>
                <label>Наличие</label>
                <input type={'checkbox'} checked={isStock} onChange={(e) => setIsStock(e.target.checked)}/>
                <button onClick={onCreate}>Добавить товар</button>
                <label>Номер товара</label>
                <input value={id} onChange={e => setPrice(+e.target.value)}/>
                <button onClick={deleteProduct}>Удалить товар</button>
            </div>

            <div>
                <p>Таблица продуктов</p>
                <table>
                    <tbody>

                    {productTS.map((x) => (
                        <tr key={x.id}>
                            <td>{x.name}</td>
                            <td>{x.price}</td>
                            <td>{x.description}</td>
                            <td>{x.isStock}</td>
                            <td>{x.count}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};
