import { PlayerState } from './player.store';

const initialAuthState: PlayerState = {
  player: null,
  itemIds: [],
  items: {}
};

function playerReducer(state: PlayerState = initialAuthState, action): PlayerState {
  switch (action.type) {
   

    default:
      return state;
  }
}

export const reducers = playerReducer