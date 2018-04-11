import React, {Component} from 'react';
import axios from 'axios';
import Card from './Card';
import './List.css';
import { BASE_URL } from '../constants';

export default class List extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            name: props.listName
        };
    }

    renderCards = () => {
        return(this.props.cards.map((card) => <Card cardName={card.name}/>))
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
    
    render() {
        return (
            <div className="col-3">
            <div className="row">
                <input value={this.state.name} onChange={this.onListNameChange} className="form-control col-8"/>
                <button onClick={this.saveListName} className="btn btn-success col-2">Edit</button>
                <button className="btn badge-danger col-2 btn-block">X</button>
            </div>
                <div className="card card-block">
                    {this.renderCards()}
                </div>
                <div>
                    <button className="btn btn-warning btn-sm">Add card</button>
                </div>
            </div>
        )
    }
}