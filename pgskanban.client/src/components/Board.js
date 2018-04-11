import React, {Component} from 'react';
import List from './List';
import { BASE_URL } from '../constants';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import './Board.css';

class Board extends React.Component {
    constructor()
    {
        super();
        this.state = {
            boardName: '',
            boardId: 0,
            boardData: [],
            listName: ""
        }
    }

    componentDidMount() {
        axios.get(BASE_URL + '/board').then(response => {
            console.log(response);
            this.setState({
                boardData: response.data.lists, 
                boardName: response.data.name, 
                boardId: response.data.id 
              })           
            })
            .catch(() => {
                this.props.history.push('/new');
        });
    }

    renderList = () => {
        return(this.state.boardData.map((list) => <List key={list.id} boardId={this.state.boardId} listId={list.id} listName={list.name} cards={[]}/>))
    }

    onClickList = () => {
        axios.post(BASE_URL + '/list', {name: this.state.listName, boardId: this.state.boardId})
        .then((response) => {
            console.log(response);
            this.setState(prevState => {
                return{
                    boardData: [...prevState.boardData, response.data],
                    listName: ''
                }
            })
        })
        .catch((err) => {
            console.log(err);
        });
    }

    onChangeListName = (e) => {
        this.setState({listName: e.target.value})
    }

    render() {
        return (
            <div>
                <div>
                    <h1>{this.state.boardName}</h1>
                    <button onClick={this.onClickList} className="btn btn-info" disabled={!this.state.listName}>Add new list</button>
                    <input type="text" value={this.state.listName} onChange={this.onChangeListName}/>
                </div>          
                <div className="container-fluid">
                    <div className="row flex-row flex-no-wrap">
                    {this.renderList()} 
                    </div>
                </div>                     
            </div>
        )
    }
}

export default withRouter(Board);