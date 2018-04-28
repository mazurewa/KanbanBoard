import React, {Component} from 'react';
import './Card.css';
import axios from 'axios';
import { BASE_URL } from '../constants';
import Modal from 'react-modal';

const customStyles = {
    content : {
      top                   : '50%',
      left                  : '50%',
      right                 : 'auto',
      bottom                : 'auto',
      marginRight           : '-50%',
      transform             : 'translate(-50%, -50%)',
      width: '500px'
    }
  };

export default class Card extends React.Component {
    constructor(props) {
        super(props);
    
        this.state = {
          modalIsOpen: false,
          name: props.cardName, 
          description: props.cardDescription
        };
    }
    

    openModal = () => {
        this.setState({modalIsOpen: true});
      }
    
    closeModal = () => {
        this.setState({modalIsOpen: false});
      }

    deleteCard = () => {
        this.props.onDeleteCard(this.props.cardId);
    }
    
    onCardNameChange = e => {
        this.setState({ name: e.target.value});
    }

    saveCardName = () => {
        axios.put(BASE_URL + '/card', {name: this.state.name, cardId: this.props.cardId, description: this.state.description})
        .then(() => {
            console.log("Successfully updated card name!");
        })
        .catch((error) => {
            console.log(error);
        });
    }

    onDescriptionChange = e => {
        this.setState({ description: e.target.value});
    }

    saveDescription = () => {
        axios.put(BASE_URL + '/card', {cardId: this.props.cardId, description: this.state.description, name: this.state.name})
        .then(() => {
            console.log("Successfully updated card description!");
            this.closeModal();
        })
        .catch((error) => {
            console.log(error);
        });
    }

    render() {
        return (
            <div className="card__container">
                <Modal
                    isOpen={this.state.modalIsOpen}
                    onRequestClose={this.closeModal}
                    style={customStyles}
                    contentLabel="Example Modal">
                        <h2>{this.props.cardName}</h2>
                        <div className="form-group">
                            <label for="description">Description:</label>
                            <textarea className="form-control" rows="5" id="description" value={this.state.description} 
                            onChange={this.onDescriptionChange}></textarea>
                            <button onClick={this.saveDescription} className="btn btn-info">Save</button>
                            <button onClick={this.closeModal} className="btn btn-danger">X</button>
                        </div>
                </Modal>
                <div className="row">
                    <input onClick={this.openModal} value={this.state.name} onChange={this.onCardNameChange} className="col-8" />
                    <button onClick={this.saveCardName} className="btn btn-success col-2">Save</button>
                    <button onClick={this.deleteCard} className="btn badge-danger col-2 btn-block">X</button>
                </div>              
            </div>
        )
    }
}