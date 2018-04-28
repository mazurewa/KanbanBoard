import React, {Component} from 'react';
import axios from 'axios';
import Card from './Card';
import './List.css';
import { BASE_URL } from '../constants';

export default class List extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            name: props.listName,
            cardName: '',
            listId: props.listId,
            listData: props.cards
        };
    }

    renderCards = () => {
        return(this.state.listData.map((card) => <Card key={card.id} listId={this.state.listId} cardId={card.id} 
        cardName={card.name} cardDescription={card.description} onDeleteCard={this.deleteCard}/>))
    }

    deleteCard = (id) => {
        if (window.confirm("Are you sure?")) {
            axios.delete(`${BASE_URL}/card`, {data: {cardId: id, listId: this.state.listId}})
            .then(() => {
                console.log("Successfully deleted card!");
                this.setState({listData: this.state.listData.filter(x => x.cardId != id)})
            })
            .catch((error) => {
                console.log(error);
            });
        }
    }

    onListNameChange = e => {
        this.setState({ name: e.target.value});
    }

    saveListName = () => {
        axios.put(BASE_URL + '/list', {name: this.state.name, boardId: this.props.boardId, listId: this.props.listId})
        .then(() => {
            console.log("Successfully updated list!");
        })
        .catch((error) => {
            console.log(error);
        });
    }

    deleteList = () => {
        this.props.onDeleteList(this.state.listId);
    }

    onClickCard = () => {
        axios.post(BASE_URL + '/card', {name: this.state.cardName, listId: this.state.listId})
        .then((response) => {
            console.log(response);
            this.setState(prevState => {
                return{
                    listData: [...prevState.listData, response.data],
                    cardName: ''
                }
            })
        })
        .catch((err) => {
            console.log(err);
        });
    }

    onCardTextChange = (e) => {
        this.setState({cardName: e.target.value})
    }
     
    render() {
        return (
            <div className="col-3">
            <div className="row">
                <input value={this.state.name} onChange={this.onListNameChange} className="form-control col-8"/>
                <button onClick={this.saveListName} className="btn btn-success col-2">Save</button>
                <button onClick={this.deleteList} className="btn badge-danger col-2 btn-block">X</button>
            </div>
                <div className="card card-block">
                    {this.renderCards()}
                </div>
                <div>
                    <input type="text" value={this.state.cardName} onChange={this.onCardTextChange} />
                    <button onClick={this.onClickCard} className="btn btn-info" disabled={!this.state.cardName}>Add card</button>
                </div>
            </div>
        )
    }
}