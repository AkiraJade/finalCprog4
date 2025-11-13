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
        
        // ✅ FIXED: Add the selected character correctly
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
        
        easyPortal ePor = new easyPortal(1);
        addObject(ePor, 704, 270);
        
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act() {
        handleSpikeSpawning();
    }
    
    // ✅ FIXED: Get player regardless of character type
    private Actor getPlayer() {
        java.util.List<Person> persons = getObjects(Person.class);
        if (!persons.isEmpty()) return persons.get(0);
        
        java.util.List<Person2> persons2 = getObjects(Person2.class);
        if (!persons2.isEmpty()) return persons2.get(0);
        
        java.util.List<Person3> persons3 = getObjects(Person3.class);
        if (!persons3.isEmpty()) return persons3.get(0);
        
        return null;
    }
    
    private void handleSpikeSpawning() {
        Actor player = getPlayer();
        
        if (player == null) {
            return;
        }
        
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