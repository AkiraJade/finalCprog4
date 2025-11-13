import greenfoot.*;

public class easyLevel1 extends World
{
    // Spike spawning variables
    private boolean spikeSpawned = false;
    private int spikeCount = 0; 
    private int spikeX = 380;
    private int spawnDelay = 2;
    private int spawnTimer = 0;
    
    public easyLevel1()
    {    
        super(750, 400, 1); 
        prepare();
    }
    
    public void prepare(){
        Platform p  = new Platform(615, 100);
        addObject(p, 307, 350);
        
        // Add the selected character - CHANGED TO Actor TYPE
        Actor player;
        int selectedChar = CharacterManager.getSelectedCharacter();
        
        switch(selectedChar)
        {
            case 2:
                player = new Person2();
                break;
            case 3:
                player = new Person3();
                break;
            default:
                player = new Person();
                break;
        }
        
        addObject(player, 30, 280);
        
        PlatformTrap pT = new PlatformTrap();
        addObject(pT, 650, 350);
        Platform p2  = new Platform(140,100);
        addObject(p2, 730, 350);
        
        // CHANGED FROM 3 TO 1
        easyPortal ePor = new easyPortal(1);
        addObject(ePor, 704, 270);
        
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
        
        
    }
    
    public void act() {
        handleSpikeSpawning();
    }
    
    private void handleSpikeSpawning() {
        // Get the player (check all character types)
        java.util.List<Actor> players = new java.util.ArrayList<Actor>();
        players.addAll(getObjects(Person.class));
        players.addAll(getObjects(Person2.class));
        players.addAll(getObjects(Person3.class));
        
        if (players.isEmpty()) {
            return;
        }
        
        Actor player = players.get(0);
        int playerX = player.getX();
        
        // First spike trigger
        if (playerX >= 290 && !spikeSpawned) {
            addObject(new Spike(), 350, 292);
            spikeSpawned = true;
            spikeCount = 1;
        }
        // Timed spawning of next spikes
        if (spikeSpawned && spikeCount < 40 && playerX >= 408) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay) {
                addObject(new Spike(), spikeX, 292);
                spikeX += 6;
                spawnTimer = 0;
                spikeCount++;
            }
        }
    }
}