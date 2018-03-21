import React, {Component} from 'react';
import './Card.css';

export default class Card extends React.Component {
    render() {
        return (
            <div className="card__container">
                {this.props.cardName}
            </div>
        )
    }
}